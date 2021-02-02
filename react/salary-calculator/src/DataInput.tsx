import { Component } from 'react';

interface Property {
    onGrossChange: (gross: number) => void;
    onNumberOfDependantChange: (numberOfDependant: number) => void;
    gross: number;
    numberOfDependant: number;
}

class DataInput extends Component<Property, {}> {
    constructor(props: any) {
        super(props);
        this.handleGrossChange = this.handleGrossChange.bind(this);
        this.handleNumberOfDependantChange = this.handleNumberOfDependantChange.bind(this);
    }

    handleGrossChange(e: any) {
        this.props.onGrossChange(+ e.target.value);
    }

    handleNumberOfDependantChange(e: any) {
        this.props.onNumberOfDependantChange(+ e.target.value);
    }
    
    render() {
        return (
            <div>
                <label htmlFor="gross">GROSS (VND):</label>
                <div className="input-group mb-3">
                    <input value={this.props.gross} onChange={this.handleGrossChange}
                        type="number" min="0" step="1000000" className="form-control" id="gross"/>
                </div>
                <label htmlFor="numberOfDependant">Số người phụ thuộc:</label>
                <div className="input-group mb-3">
                    <input value={this.props.numberOfDependant} onChange={this.handleNumberOfDependantChange}
                        type="number" min="0" className="form-control" id="numberOfDependant"/>
                </div>
            </div>
        );
    }
}

export default DataInput;
