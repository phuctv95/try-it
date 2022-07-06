import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { BooksService } from './book/services/books.services';
import { addBook, removeBook, retrievedBookList, selectBookCollection, selectBooks } from './book/state';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  books$ = this.store.select(selectBooks);
  bookCollection$ = this.store.select(selectBookCollection);
 
  constructor(
    private booksService: BooksService,
    private store: Store
  ) {}
 
  ngOnInit() {
    this.booksService
      .getBooks()
      .subscribe((books) => this.store.dispatch(retrievedBookList({ books })));
  }
 
  onAdd(bookId: string) {
    this.store.dispatch(addBook({ bookId }));
  }
 
  onRemove(bookId: string) {
    this.store.dispatch(removeBook({ bookId }));
  }

}
