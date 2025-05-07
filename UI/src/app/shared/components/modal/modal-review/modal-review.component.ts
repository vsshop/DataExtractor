import { Component, Input, OnInit } from '@angular/core';
import { TableFile } from '@models/table.file';
import { Table } from '../../../interfaces/table.interface';
import { TableService } from '../../../../services/table.service';
import { take } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ui-modal-review',
  templateUrl: './modal-review.component.html',
  styleUrl: './modal-review.component.scss'
})
export class ModalReviewComponent implements OnInit {
  tables: Table<string[]>[] = [];
  highlights: boolean[][][] = [];
  constructor(private service: TableService, private translate: TranslateService) { }

  ngOnInit() {
    const table = this.service.table;
    if (table) {
      this.tables = [
        { name: this.translate.instant("BUTTON.ORIGINAL"), columns: table.columnsOriginal, rows: this.rows(table.dataOriginal) },
        { name: this.translate.instant("BUTTON.MODIFIED"), columns: table.columnsModified, rows: this.rows(table.dataModified) }
      ]

      const hightlights = this.diff(table);
      this.highlights = [hightlights, hightlights];
    }
  }

  private rows(columns: string[][]): string[][] {
    return Array.from({ length: columns[0].length },
      (_, rowIndex) => columns.map(col => col[rowIndex]));
  }

  private diff(table: TableFile) {
    const original = table.dataOriginal;
    const modified = table.dataModified;

    const rowCount = original[0]?.length ?? 0;
    const colCount = original.length;

    return Array.from({ length: rowCount }, (_, rowIndex) =>
      Array.from({ length: colCount }, (_, colIndex) => {
        const a = original[colIndex]?.[rowIndex];
        const b = modified[colIndex]?.[rowIndex];
        return a !== b;
      })
    );
  }
}
