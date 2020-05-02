import { Component, OnInit, Input, OnChanges, SimpleChanges, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-foods',
  templateUrl: './foods.component.html',
  styleUrls: ['./foods.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FoodsComponent implements OnInit, OnChanges {

  @Input() data: any;

  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
  }

  ngOnInit(): void {
  }

  doNothing(): void { }

}
