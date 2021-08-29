import { ChangeDetectionStrategy, Component, DoCheck, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-component-f',
  templateUrl: './component-f.component.html',
  styleUrls: ['./component-f.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ComponentFComponent implements OnInit, DoCheck {

  @Input() data: any;

  constructor() { }

  ngOnInit(): void {
  }

  ngDoCheck(): void {
    console.log('CD on F');
  }

  onClick() {
    this.data.now = Date.now();
  }

}
