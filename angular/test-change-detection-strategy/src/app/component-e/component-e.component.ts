import { Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-component-e',
  templateUrl: './component-e.component.html',
  styleUrls: ['./component-e.component.scss']
})
export class ComponentEComponent implements OnInit, DoCheck {

  @Input() data: any;

  constructor() { }

  ngOnInit(): void {
  }

  ngDoCheck(): void {
    console.log('CD on E');
  }

  onClick() {
    this.data.now = Date.now();
  }

}
