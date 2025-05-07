import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { TableFile } from "@models/table.file";
import JSZip from 'jszip';

@Injectable({
  providedIn: "root"
})
export class TableService {
  table: TableFile | null = null;
  column: number = -1;

  private selectSubject = new BehaviorSubject<TableFile | null>(null)
  select$ = this.selectSubject.asObservable();

  private editSubject = new BehaviorSubject<number>(this.column)
  edit$ = this.editSubject.asObservable();
  
  private tablesSubject = new BehaviorSubject <TableFile[] | null>(null)
  tables$ = this.tablesSubject.asObservable();

  edit(column: number) {
    this.column = column;
    this.editSubject.next(column);
  }

  select(table: TableFile | null = null) {
    this.column = -1;
    this.table = table;
    this.selectSubject.next(table);
  }

  tables(tables: TableFile[] | null) {
    this.column = -1;
    this.table = null;
    this.tablesSubject.next(tables);
  }

  async save() {
    const tables = this.tablesSubject.getValue();
    if (!tables) return;

    const zip = new JSZip();
    for (const file of tables) {
      const buffer = await file.content();
      zip.file(`${file.name}.csv`, new Uint8Array(buffer)); 
    }

    const zipBlob = await zip.generateAsync({ type: 'blob' });
    const a = document.createElement('a');
    a.href = URL.createObjectURL(zipBlob);
    a.download = 'tables.zip';
    a.click();
    URL.revokeObjectURL(a.href);
  }
}
