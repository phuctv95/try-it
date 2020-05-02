import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormComponent } from './reactive-form/reactive-form.component';
import { MyFormsComponent } from './my-forms/my-forms.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TemplateDrivenFormComponent } from './template-driven-form/template-driven-form.component';
import { NestedGroupReactiveFormComponent } from './nested-group-reactive-form/nested-group-reactive-form.component';



@NgModule({
  declarations: [MyFormsComponent, ReactiveFormComponent, TemplateDrivenFormComponent, NestedGroupReactiveFormComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [ReactiveFormComponent]
})
export class MyFormsModule { }
