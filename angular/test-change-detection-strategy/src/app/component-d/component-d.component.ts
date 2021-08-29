import { Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-component-d',
  templateUrl: './component-d.component.html',
  styleUrls: ['./component-d.component.scss']
})
export class ComponentDComponent implements OnInit, DoCheck {

  @Input() data: any;

  constructor() { }

  ngOnInit(): void {
  }

  ngDoCheck(): void {
    console.log('CD on D');
  }

  onClick() {
    this.data.now = Date.now();
  }

}
