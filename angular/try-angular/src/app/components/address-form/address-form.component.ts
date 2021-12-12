import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormBuilder, NG_VALIDATORS, NG_VALUE_ACCESSOR, ValidationErrors, Validator, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: AddressFormComponent
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: AddressFormComponent
    },
  ]
})
export class AddressFormComponent implements OnInit, OnDestroy, ControlValueAccessor, Validator {

  form = this.fb.group({
    addressLine1: [null, [Validators.required]],
    addressLine2: [null, [Validators.required]],
    zipCode: [null, [Validators.required]],
    city: [null, [Validators.required]],
  });

  onChangeSubs: Subscription[] = [];
  onTouched = () => { };

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.onChangeSubs.forEach(s => s.unsubscribe());
  }

  writeValue(value: any): void {
    this.form.setValue(value, { emitEvent: false });
  }

  registerOnChange(fn: any): void {
    const sub = this.form.valueChanges.subscribe(fn);
    this.onChangeSubs.push(sub);
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(disabled: boolean) {
    if (disabled) {
      this.form.disable();
    } else {
      this.form.enable();
    }
  }

  validate(control: AbstractControl): ValidationErrors | null {
    if (this.form.valid) {
      return null;
    }
    let errors: any = {};
    errors = this.addControlErrors(errors, "addressLine1");
    errors = this.addControlErrors(errors, "addressLine2");
    errors = this.addControlErrors(errors, "zipCode");
    errors = this.addControlErrors(errors, "city");
    return errors;
  }

  addControlErrors(allErrors: any, controlName: string) {
    const errors = { ...allErrors };
    const controlErrors = this.form.controls[controlName].errors;
    if (controlErrors) {
      errors[controlName] = controlErrors;
    }
    return errors;
  }

}
