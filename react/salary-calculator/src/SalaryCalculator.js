import { Component } from 'react';
import Header from './Header';
import DataInput from './DataInput';
import CalculatedResult from './CalculatedResult';

class SalaryCalculator extends Component {
    constructor(props) {
        super(props);

        this.handleGrossChange = this.handleGrossChange.bind(this);
        this.handleNumberOfDependantChange = this.handleNumberOfDependantChange.bind(this);

        this.state = {
            gross: 0,
            numberOfDependant: 0,
        };
    }

    handleGrossChange(gross) {
        this.setState({
            gross: gross,
        });
    }

    handleNumberOfDependantChange(numberOfDependant) {
        this.setState({
            numberOfDependant: numberOfDependant,
        });
    }

    render() {
        return (
            <div className="container">
                <div className="row mb-4 mt-3">
                    <div className="col">
                        <Header/>
                    </div>
                </div>
                <div className="row justify-content-center">
                    <div className="col col-md-6">
                        <DataInput gross={this.state.gross} onGrossChange={this.handleGrossChange}
                            numberOfDependant={this.state.numberOfDependant} onNumberOfDependantChange={this.handleNumberOfDependantChange}/>
                    </div>
                </div>
                <div className="row justify-content-center">
                    <div className="col col-md-6">
                        <CalculatedResult gross={this.state.gross} numberOfDependant={this.state.numberOfDependant}/>
                    </div>
                </div>
            </div>
        );
    }
}

export default SalaryCalculator;
