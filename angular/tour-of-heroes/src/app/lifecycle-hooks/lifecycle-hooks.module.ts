import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { LifecycleHooksDemoComponent } from './lifecycle-hooks-demo/lifecycle-hooks-demo.component';
import { HighlightDirective } from './highlight.directive';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [CardComponent, LifecycleHooksDemoComponent, HighlightDirective],
  imports: [
    CommonModule, FormsModule
  ],
  exports: [CardComponent]
})
export class LifecycleHooksModule { }
