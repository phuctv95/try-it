import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-form-builder-group',
  templateUrl: './form-builder-group.component.html',
  styleUrls: ['./form-builder-group.component.less']
})
export class FormBuilderGroupComponent implements OnInit {

  profile = this.fb.group({
    firstName: [''],
    lastName: [''],
    address: this.fb.group({
      street: [''],
      city: ['']
    })
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.profile.value);
  }

}
