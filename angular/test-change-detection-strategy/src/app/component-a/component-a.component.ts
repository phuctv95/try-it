import { ChangeDetectionStrategy, Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-component-a',
  templateUrl: './component-a.component.html',
  styleUrls: ['./component-a.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ComponentAComponent implements OnInit, DoCheck {

  @Input() data: any;

  constructor() { }

  ngOnInit(): void {
  }

  ngDoCheck(): void {
    console.log('CD on A');
  }

  onClick() {
    this.data.now = Date.now();
    // setTimeout(() => this.data.now = Date.now(), 500);
  }

}
