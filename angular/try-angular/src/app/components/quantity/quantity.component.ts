import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator } from '@angular/forms';

@Component({
  selector: 'app-quantity',
  templateUrl: './quantity.component.html',
  styleUrls: ['./quantity.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: QuantityComponent
    },
    {
      provide: NG_VALIDATORS,
      multi:true,
      useExisting: QuantityComponent
    },
  ],
})
export class QuantityComponent implements OnInit, ControlValueAccessor, Validator {

  @Input() increment = 1;

  quantity = 0;
  touched = false;
  disabled = false;
  // calling onChange will update the formControl.value and trigger the actual call back
  onChange = (quantity: number) => {};
  onTouched = () => {};

  constructor() { }

  ngOnInit(): void {
  }

  writeValue(value: number): void {
    this.quantity = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }

  validate(control: AbstractControl): ValidationErrors | null {
    const quantity = control.value as number;
    if (quantity < 0) {
      return { mustBePositive: { quantity } };
    }
    return null;
  }

  markAsTouchedIfNotYet() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  onSubtract() {
    if (this.disabled) {
      return;
    }
    this.markAsTouchedIfNotYet();
    this.quantity -= this.increment;
    this.onChange(this.quantity);
  }

  onAdd() {
    if (this.disabled) {
      return;
    }
    this.markAsTouchedIfNotYet();
    this.quantity += this.increment;
    this.onChange(this.quantity);
  }

}
