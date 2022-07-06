import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookCollectionComponent } from './components/book-collection/book-collection.component';



@NgModule({
  declarations: [
    BookListComponent,
    BookCollectionComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    BookListComponent,
    BookCollectionComponent
  ]
})
export class BookModule { }
