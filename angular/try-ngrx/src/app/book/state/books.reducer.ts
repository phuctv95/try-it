import { createReducer, on } from '@ngrx/store';
import { Book } from '../models';
import { retrievedBookList } from './books.actions';

const initialState: ReadonlyArray<Book> = [];

export const booksReducer = createReducer(
    initialState,
    on(retrievedBookList, (state, { books }) => books),
);
