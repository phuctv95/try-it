import { Component } from 'react';
import { NumberInput } from './NumberInput';

interface Property {
    onGrossChange: (gross: number) => void;
    onNumberOfDependantChange: (numberOfDependant: number) => void;
    gross: number;
    numberOfDependant: number;
}

class DataInput extends Component<Property, {value: number}> {
    constructor(props: any) {
        super(props);
        this.handleGrossChange = this.handleGrossChange.bind(this);
        this.handleNumberOfDependantChange = this.handleNumberOfDependantChange.bind(this);
        this.state = {value: 123};
    }

    handleGrossChange(value: number) {
        this.props.onGrossChange(value);
    }

    handleNumberOfDependantChange(value: number) {
        this.props.onNumberOfDependantChange(value);
    }
    
    render() {
        return (
            <div>
                <label htmlFor="gross">GROSS (VND):</label>
                <div className="input-group mb-3">
                    <NumberInput value={this.props.gross} onChange={this.handleGrossChange}
                        thousandSeparator={true} selectAllOnFocus={true} step={1000000} min={0} id="gross"/>
                </div>
                <label htmlFor="numberOfDependant">Số người phụ thuộc:</label>
                <div className="input-group mb-3">
                    <NumberInput value={this.props.numberOfDependant} onChange={this.handleNumberOfDependantChange}
                        thousandSeparator={true} selectAllOnFocus={true} step={1} min={0} id="numberOfDependant"/>
                </div>
            </div>
        );
    }
}

export default DataInput;
