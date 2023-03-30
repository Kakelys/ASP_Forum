import { SectionDataService } from './../../../services/section-data.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SectionDetail } from 'src/shared/section-detail.mode';

@Component({
  selector: 'app-section',
  templateUrl: './section.component.html',
  styleUrls: ['./section.component.css']
})
export class SectionComponent implements OnInit {

  @Input()
  public section: SectionDetail;

  @Input()
  public canEdit: boolean

  public editing = false;

  constructor(private sectionData: SectionDataService) { }

  onClickDelete() {
    this.sectionData.deleteSection(this.section.id).subscribe({
      next: _ => {this.sectionData.updated.next(true);},
      error: err => console.error(err)
    });
  }

  ngOnInit(): void {
  }

  onEditToggle() {
    this.editing = !this.editing;
  }

  onSave(saved: boolean) {
    if (saved) {
      this.sectionData.updated.next(true);
    }
  }

}
