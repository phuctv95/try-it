import { Book } from '../models';

export interface AppState {
    books: ReadonlyArray<Book>;
    collection: ReadonlyArray<string>;
}
