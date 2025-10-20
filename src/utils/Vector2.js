/**
 * Vector2 utility class
 * Similar to XNA Vector2
 */

class Vector2 {
    constructor(x = 0, y = 0) {
        this.x = x;
        this.y = y;
    }

    static get Zero() {
        return new Vector2(0, 0);
    }

    static get One() {
        return new Vector2(1, 1);
    }

    static get UnitX() {
        return new Vector2(1, 0);
    }

    static get UnitY() {
        return new Vector2(0, 1);
    }

    // Length of the vector
    length() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
    }

    // Squared length (faster than length)
    lengthSquared() {
        return this.x * this.x + this.y * this.y;
    }

    // Normalize the vector (make length = 1)
    normalize() {
        const len = this.length();
        if (len > 0) {
            this.x /= len;
            this.y /= len;
        }
        return this;
    }

    // Get normalized copy
    normalized() {
        const v = new Vector2(this.x, this.y);
        return v.normalize();
    }

    // Add another vector
    add(v) {
        this.x += v.x;
        this.y += v.y;
        return this;
    }

    // Subtract another vector
    subtract(v) {
        this.x -= v.x;
        this.y -= v.y;
        return this;
    }

    // Multiply by scalar
    multiply(scalar) {
        this.x *= scalar;
        this.y *= scalar;
        return this;
    }

    // Dot product
    dot(v) {
        return this.x * v.x + this.y * v.y;
    }

    // Distance to another vector
    distanceTo(v) {
        const dx = this.x - v.x;
        const dy = this.y - v.y;
        return Math.sqrt(dx * dx + dy * dy);
    }

    // Squared distance (faster)
    distanceToSquared(v) {
        const dx = this.x - v.x;
        const dy = this.y - v.y;
        return dx * dx + dy * dy;
    }

    // Clone the vector
    clone() {
        return new Vector2(this.x, this.y);
    }

    // Set values
    set(x, y) {
        this.x = x;
        this.y = y;
        return this;
    }

    // Copy from another vector
    copy(v) {
        this.x = v.x;
        this.y = v.y;
        return this;
    }

    // Static methods
    static add(v1, v2) {
        return new Vector2(v1.x + v2.x, v1.y + v2.y);
    }

    static subtract(v1, v2) {
        return new Vector2(v1.x - v2.x, v1.y - v2.y);
    }

    static multiply(v, scalar) {
        return new Vector2(v.x * scalar, v.y * scalar);
    }

    static dot(v1, v2) {
        return v1.x * v2.x + v1.y * v2.y;
    }

    static distance(v1, v2) {
        const dx = v1.x - v2.x;
        const dy = v1.y - v2.y;
        return Math.sqrt(dx * dx + dy * dy);
    }

    static fromAngle(angle) {
        return new Vector2(Math.cos(angle), Math.sin(angle));
    }

    // Get angle of this vector
    angle() {
        return Math.atan2(this.y, this.x);
    }

    // Rotate by angle (radians)
    rotate(angle) {
        const cos = Math.cos(angle);
        const sin = Math.sin(angle);
        const x = this.x * cos - this.y * sin;
        const y = this.x * sin + this.y * cos;
        this.x = x;
        this.y = y;
        return this;
    }
}
