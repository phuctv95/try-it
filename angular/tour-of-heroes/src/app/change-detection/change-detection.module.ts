import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChangeDetectionComponent } from './change-detection/change-detection.component';
import { FoodsComponent } from './foods/foods.component';
import { EmojiComponent } from './emoji/emoji.component';



@NgModule({
  declarations: [ChangeDetectionComponent, FoodsComponent, EmojiComponent],
  imports: [
    CommonModule
  ],
  exports: [FoodsComponent]
})
export class ChangeDetectionModule { }
