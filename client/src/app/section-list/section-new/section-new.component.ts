import { SectionDataService } from '../../../services/section-data.service';
import { NgForm } from '@angular/forms';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-section-new',
  templateUrl: './section-new.component.html',
  styleUrls: ['./section-new.component.css'],
})
export class SectionNewComponent implements OnInit {
  constructor(
    private sectionData: SectionDataService
  ) {}

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    console.log(form.value);

    this.sectionData.createNewSection(form.value).subscribe({
      next: (_) => {
        this.sectionData.updated.next(true);
      },
      error: (err) => {
        console.log(err);
      },
    });

  }

  onCancel() {
    this.sectionData.updated.next(false);
  }
}
