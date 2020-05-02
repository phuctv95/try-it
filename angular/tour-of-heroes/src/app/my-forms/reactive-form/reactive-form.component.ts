import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-reactive-form',
  templateUrl: './reactive-form.component.html',
  styleUrls: ['./reactive-form.component.less']
})
export class ReactiveFormComponent implements OnInit {

  profile = new FormGroup({
    firstName: new FormControl('Abc'),
    lastName: new FormControl('Xyz')
  });

  constructor() { }

  ngOnInit(): void { }

  onSubmit(): void {
    console.log(this.profile.value);
  }

}
