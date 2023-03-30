import { Section } from '../shared/section.model';
import { SectionDetail } from './../shared/section-detail.mode';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError, catchError, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SectionDataService {

  public updated = new Subject<boolean>();

  constructor(private http: HttpClient){}

  private baseUrl = 'http://localhost:5100/api/v1/sections';

  fetchSectionDetail(): Observable<SectionDetail[]> {
    return this.http.get<SectionDetail[]>(this.baseUrl)
      .pipe(catchError(this.handleError));
  }

  createNewSection(data: Section): Observable<any> {
    return this.http.post(this.baseUrl, data)
      .pipe(catchError(this.handleError));
  }

  updateSection(id: number, data: Section): Observable<any> {
    return this.http.put(this.baseUrl + '/' + id, data)
      .pipe(catchError(this.handleError));
  }

  deleteSection(id: number) {
    return this.http.delete(this.baseUrl + '/' + id)
      .pipe(catchError(this.handleError));
  }

  private handleError(err: any) {
    console.error(err);

    if (err.error instanceof Error) {
      const errMessage = err.error.message;
      return throwError(errMessage);
    }

    if(err instanceof HttpErrorResponse) {
      if(err.status === 500)
        return throwError("Internal server error");

      if(err.error){
        const errMessage = err.message;
        return throwError(errMessage);
      }

      if(err.error.errors){
        return throwError(err.error.errors.join('\n'));
      }
    }

    return throwError("Something went wrong");
  }

}
