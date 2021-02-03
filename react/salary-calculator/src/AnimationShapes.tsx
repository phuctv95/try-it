import { Component, CSSProperties, ReactNode } from "react";
import './AnimationShapes.css';
import { random, randomEnum } from "./tools";

enum ShapeType { Square, Circle, Rectangle }

class AnimationShapes extends Component {
    readonly NumberOfShapes = 10;

    generateAnimationShape(key: string | number): ReactNode {
        let shapeType = randomEnum(ShapeType);
        let size = random(3, 7);
        let style = {
            width: `${size}vw`,
            height: `${size}vw`,
            left: `${random(5, 95)}vw`,
            animationDuration: `${random(10, 20)}s`,
            animationDelay: `${random(0, 10)}s`,
        } as CSSProperties;
        if (shapeType === ShapeType.Square) {
            return <div key={key} className="shape" style={style}></div>;
        }
        if (shapeType === ShapeType.Circle) {
            return <div key={key} className="shape circle" style={style}></div>;
        }
        if (shapeType === ShapeType.Rectangle) {
            return <div key={key} className="shape" style={{
                ...style,
                width: `${random(2, 4)}vw`,
                height: `${random(6, 9)}vw`,
            }}></div>;
        }
    }

    generateAnimationShapes(): ReactNode[] {
        let shapes = [] as ReactNode[];
        for (let i = 0; i < this.NumberOfShapes; i++) {
            shapes.push(this.generateAnimationShape(i));
        }
        return shapes;
    }

    randomShapeType() {
        return ShapeType.Square;
    }

    render(): ReactNode {
        return (
            <div>{this.generateAnimationShapes()}</div>
        );
    }
}

export default AnimationShapes;
