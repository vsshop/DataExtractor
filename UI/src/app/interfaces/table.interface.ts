export interface Table<T = Record<string, any>> {
  url: string;
  name: string;
  columns: string[];
  rows: T[]
}
