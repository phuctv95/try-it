import { Component } from 'react';

class DataInput extends Component {
    constructor(props) {
        super(props);
        this.handleGrossChange = this.handleGrossChange.bind(this);
        this.handleNumberOfDependantChange = this.handleNumberOfDependantChange.bind(this);
    }

    handleGrossChange(e) {
        this.props.onGrossChange(e.target.value);
    }

    handleNumberOfDependantChange(e) {
        this.props.onNumberOfDependantChange(e.target.value);
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
