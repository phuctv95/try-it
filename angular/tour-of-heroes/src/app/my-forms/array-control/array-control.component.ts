import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray } from '@angular/forms';

@Component({
  selector: 'app-array-control',
  templateUrl: './array-control.component.html',
  styleUrls: ['./array-control.component.less']
})
export class ArrayControlComponent implements OnInit {

  profile = this.fb.group({
    firstName: [''],
    lastName: [''],
    address: this.fb.group({
      street: [''],
      city: ['']
    }),
    emails: this.fb.array([
      this.fb.control('')
    ])
  });

  get emails() {
    return this.profile.get('emails') as FormArray;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.profile.value);
  }

  addEmailControl() {
    this.emails.push(this.fb.control(''));
  }

}
