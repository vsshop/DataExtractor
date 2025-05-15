import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { XMLFile } from "@models/xml.file";
import { CSVFile } from "@models/csv.file";
import { FileState } from "@enums/file.state";
import { TableFile } from "../shared/models/table.file";
import { DataFile } from "../shared/models/data.file";
import { Browser } from "@browser";

@Injectable({
  providedIn: "root"
})
export class DataService {
  private dataSubject = new BehaviorSubject<DataFile[] | null>(null)
  data$ = this.dataSubject.asObservable();

  constructor(private browser: Browser) {
    this.browser.subscribe<DataFile[]>("DataAngular",
      data => this.dataSubject.next(data));
  }

  load() {
    this.browser.invoke("DataAngular");
  }
}
