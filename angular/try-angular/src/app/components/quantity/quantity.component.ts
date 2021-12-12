import { Component, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-quantity',
  templateUrl: './quantity.component.html',
  styleUrls: ['./quantity.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: QuantityComponent
    }
  ],
})
export class QuantityComponent implements OnInit, ControlValueAccessor {

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
    if (this.quantity < 0) {
      this.quantity = 0;
    }
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
