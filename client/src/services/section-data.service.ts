import { SectionDetail } from './../shared/section-detail.mode';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SectionDataService {

  constructor(private http: HttpClient){}

  private baseUrl = 'http://localhost:5100/api/v1/sections';

  fetchSectionDetail() : Observable<SectionDetail[]>
  {
    return this.http.get<SectionDetail[]>(this.baseUrl)
      .pipe(tap(data => console.log(data)));
  }

}
