import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-nested-group-reactive-form',
  templateUrl: './nested-group-reactive-form.component.html',
  styleUrls: ['./nested-group-reactive-form.component.less']
})
export class NestedGroupReactiveFormComponent implements OnInit {

  profile = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    address: new FormGroup({
      street: new FormControl(''),
      city: new FormControl('')
    })
  });

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.profile.value);
  }
  
}
