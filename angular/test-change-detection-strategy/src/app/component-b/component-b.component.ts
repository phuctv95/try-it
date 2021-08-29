import { Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-component-b',
  templateUrl: './component-b.component.html',
  styleUrls: ['./component-b.component.scss']
})
export class ComponentBComponent implements OnInit, DoCheck {

  @Input() data: any;

  constructor() { }

  ngOnInit(): void {
  }

  ngDoCheck(): void {
    console.log('CD on B');
  }

  onClick() {
    this.data.now = Date.now();
    // setTimeout(() => this.data.now = Date.now(), 500);
  }

}
