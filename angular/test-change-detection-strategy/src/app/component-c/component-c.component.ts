import { Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-component-c',
  templateUrl: './component-c.component.html',
  styleUrls: ['./component-c.component.scss']
})
export class ComponentCComponent implements OnInit, DoCheck {

  @Input() data: any;

  constructor() { }

  ngOnInit(): void {
  }

  ngDoCheck(): void {
    console.log('CD on C');
  }

  onClick() {
    this.data.now = Date.now();

    // This will not trigger change detection.
    // It will trigger on next click (because of click event),
    // and will show the previous value.
    // setTimeout(() => {
    //   this.data.now = Date.now();
    // }, 100);
  }

}
