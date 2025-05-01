import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class FileService {

  private loadSubject = new BehaviorSubject<File[] | null>(null)
  load$ = this.loadSubject.asObservable();

  private selectSubject = new BehaviorSubject<File | null>(null)
  select$ = this.selectSubject.asObservable();

  load(files: File[] | null, types: string[] = ['.csv']) {
    const alowed = files?.filter(file => this.format(file, types)) ?? null;
    this.loadSubject.next(alowed);
  }

  select(file: File | null) {
    this.selectSubject.next(file);
  }

  remove(file: File) {
    const current = this.loadSubject.getValue();
    if (!current) return;

    const updated = current.filter(f => f !== file);
    this.loadSubject.next(updated);
  }

  private format(file: File, types: string[]) {
    return types.some(ext => file.name.toLowerCase().endsWith(ext));
  }
}
