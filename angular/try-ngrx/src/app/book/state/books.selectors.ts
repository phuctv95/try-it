import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Book } from '../models';

export const selectBooks = createFeatureSelector<ReadonlyArray<Book>>('books');

export const selectCollectionState = createFeatureSelector<ReadonlyArray<string>>('collection');

export const selectBookCollection = createSelector(
    selectBooks,
    selectCollectionState,
    (books, collection) => collection.map(id => books.find(b => b.id === id)!),
);
