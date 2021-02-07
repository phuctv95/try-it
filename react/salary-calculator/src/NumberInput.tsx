import { Component } from "react";

interface Properties {
    id: string;
    value: number;
    onChange: (value: number) => void;
    thousandSeparator: boolean;
    selectAllOnFocus: boolean;
    step: number;
}

interface State {
}

export class NumberInput extends Component<Properties, State> {
    constructor(props: Properties) {
        super(props);
        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleOnFocus = this.handleOnFocus.bind(this);
        this.handleOnWheel = this.handleOnWheel.bind(this);
    }

    getDisplayValue(value: number): string {
        return this.props.thousandSeparator
            ? value.toLocaleString()
            : value.toString();
    }

    handleOnChange(e: React.ChangeEvent<HTMLInputElement>) {
        const userEnteredNumber = this.removeNoneDigitsLetter(e.target.value);
        this.props.onChange(userEnteredNumber);
    }

    handleOnFocus(e: React.FocusEvent<HTMLInputElement>) {
        if (this.props.selectAllOnFocus) {
            e.target.select();
        }
    }

    handleOnWheel(e: React.WheelEvent<HTMLInputElement>) {
        this.props.onChange(e.deltaY < 0
            ? this.props.value + this.props.step
            : this.props.value - this.props.step);
    }

    removeNoneDigitsLetter(userEnterNumber: string): number {
        const isNegative = userEnterNumber[0] === '-';
        const removedAllNoneDigits = userEnterNumber.replace(/\D+/g, '');
        return isNegative ? - removedAllNoneDigits : + removedAllNoneDigits;
    }
    
    render() {
        return (
            <input value={this.getDisplayValue(this.props.value)} onChange={this.handleOnChange}
                id={this.props.id} onFocus={this.handleOnFocus} onWheel={this.handleOnWheel}
                type="text" className="form-control"/>
        );
    }
}