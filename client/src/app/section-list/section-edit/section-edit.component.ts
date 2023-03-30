import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SectionDataService } from 'src/services/section-data.service';
import { Section } from 'src/shared/section.model';

@Component({
  selector: 'app-section-edit',
  templateUrl: './section-edit.component.html',
  styleUrls: ['./section-edit.component.css']
})
export class SectionEditComponent {

  @Input()
  public section: Section;

  @Output()
  onClose = new EventEmitter();

  public errorMessage: string;
  public editFailed = false;

  constructor(private sectionData: SectionDataService){}

  onSubmit(form: NgForm){
    this.sectionData.updateSection(this.section.id, form.value).subscribe({
      next: (_) => {
        this.sectionData.updated.next(true);
      },
      error: (err) => {
        this.editFailed = true;
        this.errorMessage = err;
      }
    });
  }

  onCancel(){
    this.onClose.emit();
  }
}
