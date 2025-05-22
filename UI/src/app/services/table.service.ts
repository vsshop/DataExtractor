import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { TableFile } from "@models/table.file";
import JSZip from 'jszip';
import { DataService } from "./data.services";
import { Browser } from "../core/services/browser.service";

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

  constructor(data: DataService, private browser: Browser) {
    data.data$.pipe().subscribe(info => {
      if (!info) return;

      const tables = this.tablesSubject.getValue();
      if (!tables) return;

      tables.forEach(table => {
        let into = info.find(i => i.name == table.name);
        if (!into) return;

        table.columnsOriginal.forEach((col, index) => {
          let column = into.columns.findIndex(c => c.name == col);
          if (column != -1) {
            table.columnsModified[index] = into.columns[column].data[1][0];
          }
        });

        table.columnsOriginal.forEach((col, index) => {
          let column = into.columns.findIndex(c => c.name == col);
          table.modifiers[index] = false;
          if (column != -1) {
            for (var i = 0; i < table.dataModified.length; i++) {
              let value = table.dataModified[index][i];
              let part = into.columns[column].data[0].findIndex(v => v == value);
              if (part != -1) {
                table.dataModified[index][i] = into.columns[column].data[1][part];
                table.modifiers[index] = true;
              }
            }
          }
        })
      })
    })
  }

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

    const content = await zip.generateAsync({ type: 'uint8array' });
    const base64 = btoa(String.fromCharCode(...content));
    this.browser.invoke("SaveAngular", base64)
  }
}
