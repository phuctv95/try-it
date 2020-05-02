import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DemoComponent } from './demo/demo.component';
import { MyTemplateDrivenFormComponent } from './my-template-driven-form/my-template-driven-form.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [DemoComponent, MyTemplateDrivenFormComponent],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class MyTemplateFormsModule { }
