/**
 * Node - Base class for all game objects
 * Ported from Node.cs
 */

class Node {
    constructor(scene) {
        this.scene = scene;
        this.position = new Vector2(0, 0);
        this.velocity = new Vector2(0, 0);
        this.rotation = 0;
        this.scale = 1.0;
        this.children = [];
        this.parent = null;
        this.active = true;
        this.visible = true;
        this.markedForRemoval = false;

        // Sprite reference (if this node has one)
        this.sprite = null;
    }

    // Add a child node
    addChild(node) {
        node.parent = this;
        this.children.push(node);
    }

    // Remove a child node
    removeChild(node) {
        const index = this.children.indexOf(node);
        if (index > -1) {
            this.children.splice(index, 1);
            node.parent = null;
        }
    }

    // Update this node and all children
    update(deltaTime) {
        if (!this.active) return;

        // Update position based on velocity
        this.position.x += this.velocity.x;
        this.position.y += this.velocity.y;

        // Update children
        for (let i = this.children.length - 1; i >= 0; i--) {
            const child = this.children[i];
            if (child.markedForRemoval) {
                this.children.splice(i, 1);
                child.destroy();
            } else {
                child.update(deltaTime);
            }
        }

        // Update sprite position if exists
        if (this.sprite) {
            this.sprite.x = this.position.x;
            this.sprite.y = this.position.y;
            this.sprite.rotation = this.rotation;
            this.sprite.setScale(this.scale);
            this.sprite.setVisible(this.visible);
        }
    }

    // Mark this node for removal
    remove() {
        this.markedForRemoval = true;
    }

    // Destroy this node
    destroy() {
        // Destroy all children
        for (const child of this.children) {
            child.destroy();
        }
        this.children = [];

        // Destroy sprite if exists
        if (this.sprite) {
            this.sprite.destroy();
            this.sprite = null;
        }

        // Remove from parent
        if (this.parent) {
            this.parent.removeChild(this);
        }
    }

    // Get world position (accounting for parent hierarchy)
    getWorldPosition() {
        if (!this.parent) {
            return this.position.clone();
        }
        const parentPos = this.parent.getWorldPosition();
        return Vector2.add(parentPos, this.position);
    }

    // Check if position is within world bounds
    isInBounds(bounds) {
        const margin = 100; // Allow some margin outside visible area
        return this.position.x > -margin &&
               this.position.x < bounds.width + margin &&
               this.position.y > -margin &&
               this.position.y < bounds.height + margin;
    }
}
