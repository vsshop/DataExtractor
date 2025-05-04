export interface Table<T = Record<string, any>> {
  name: string;
  columns: string[];
  rows: T[]
}
