import { SectionDataService } from './../../services/section-data.service';
import { Component, OnInit } from '@angular/core';
import { SectionDetail } from 'src/shared/section-detail.mode';

@Component({
  selector: 'app-section-list',
  templateUrl: './section-list.component.html',
  styleUrls: ['./section-list.component.css']
})
export class SectionListComponent implements OnInit {

  sections: SectionDetail[];

  constructor(private sectionData: SectionDataService) { }

  ngOnInit(): void {
    this.sectionData.fetchSectionDetail()
    .subscribe(sections => this.sections = sections);
  }

}
