import { Component, OnInit } from '@angular/core';
import { Hero } from '../hero';

@Component({
  selector: 'app-my-template-driven-form',
  templateUrl: './my-template-driven-form.component.html',
  styleUrls: ['./my-template-driven-form.component.less']
})
export class MyTemplateDrivenFormComponent implements OnInit {

  powers = ['Really Smart', 'Super Flexible', 'Super Hot', 'Weather Changer'];
  model = new Hero(18, 'Dr IQ', this.powers[1], 'Chuck Overstreet');

  get diagnostic() { return JSON.stringify(this.model); }

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    console.log(this.model);
  }

}
