import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { CSVFile } from "@models/csv.file";
import { XMLFile } from "@models/xml.file";

@Injectable({
  providedIn: "root"
})
export class FileService {
  private hoverSubject = new BehaviorSubject<CSVFile | XMLFile | null>(null)
  hover$ = this.hoverSubject.asObservable();

  private selectSubject = new BehaviorSubject<CSVFile | null>(null)
  select$ = this.selectSubject.asObservable();

  hover(file: CSVFile | XMLFile | null) {
    this.hoverSubject.next(file);
  }

  select(file: CSVFile | null) {
    this.selectSubject.next(file);
  }
}
