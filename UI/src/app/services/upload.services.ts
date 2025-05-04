import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { XMLFile } from "@models/xml.file";
import { CSVFile } from "@models/csv.file";
import { FileState } from "@enums/file.state";
import { TableFile } from "../shared/models/table.file";

@Injectable({
  providedIn: "root"
})
export class UploadService {
  private xml: XMLFile[] = [];
  private csv: CSVFile[] = [];
  private openXMLSubject = new BehaviorSubject<XMLFile[] | null>(null)
  openXML$ = this.openXMLSubject.asObservable();

  private openCSVSubject = new BehaviorSubject<CSVFile[] | null>(null)
  openCSV$ = this.openCSVSubject.asObservable();

  private readySubject = new BehaviorSubject<boolean>(false)
  ready$ = this.readySubject.asObservable();

  async openXML(file: File) {
    this.xml = await XMLFile.read(file);
    this.readySubject.next(this.check);
    this.openXMLSubject.next(this.xml);
  }

  async openCSV(files: File[]) {
    const csv = await Promise.all(files.map(file => CSVFile.read(file)));
    const exist = new Set(this.csv.map(f => f.name.toLowerCase()));
    const unique = csv.filter(f => !exist.has(f.name.toLowerCase()));

    if (unique.length > 0) {
      this.csv = [...this.csv, ...unique];
      this.readySubject.next(this.check);
      this.openCSVSubject.next(this.csv);
    }
  }

  removeCSV(csv: CSVFile) {
    this.csv = this.csv.filter(f => f !== csv);
    this.readySubject.next(this.check);
    this.openCSVSubject.next(this.csv);
  }

  clear() {
    this.csv = [];
    this.readySubject.next(this.check);
    this.openCSVSubject.next(this.csv);
  }

  get data() {
    return this.csv.map(csv => {
      let xml = this.checkExist(csv)!;
      return new TableFile(xml, csv);
    })
  }

  get check() {
    let ready = true;
    if (this.csv.length == 0) ready = false;

    for (const xmlFile of this.xml) {
      xmlFile.state = FileState.none;
    }
    for (const csvFile of this.csv) {
      csvFile.state = FileState.none;
    }

    this.csv.forEach(csv => {
      let xml = this.checkExist(csv);
      let check = xml && this.checkColumns(csv, xml);
      if (!check) {
        csv.state = FileState.error;
        ready = false;
      }
    })

    return ready;
  }

  private checkExist(csv: CSVFile) {
    return this.xml.find(xml => xml.name.toLowerCase() === csv.name.toLowerCase());
  }

  private checkColumns(csv: CSVFile, xml: XMLFile) {
    const state = csv.columns.length == xml.columns.length;
    xml.state = state ? FileState.check : FileState.error;
    csv.state = state ? FileState.check : FileState.error;
    return state;
  }
}
