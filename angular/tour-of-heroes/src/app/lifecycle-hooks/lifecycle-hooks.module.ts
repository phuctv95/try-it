import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { LifecycleHooksDemoComponent } from './lifecycle-hooks-demo/lifecycle-hooks-demo.component';



@NgModule({
  declarations: [CardComponent, LifecycleHooksDemoComponent],
  imports: [
    CommonModule
  ],
  exports: [CardComponent]
})
export class LifecycleHooksModule { }
