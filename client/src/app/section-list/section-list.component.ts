import { AuthService } from './../auth/auth.service';
import { Subscription } from 'rxjs';
import { SectionDataService } from './../../services/section-data.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { SectionDetail } from 'src/shared/section-detail.mode';

@Component({
  selector: 'app-section-list',
  templateUrl: './section-list.component.html',
  styleUrls: ['./section-list.component.css']
})
export class SectionListComponent implements OnInit, OnDestroy {

  canEdit = false;
  creatingNew = false;
  private userSub: Subscription;
  private sectionUpdateSub: Subscription;

  sections: SectionDetail[];

  constructor(private sectionData: SectionDataService, private authService: AuthService) { }

  ngOnInit(): void {
    this.updateSections();
    this.userSub = this.authService.$user.subscribe(user => {
      this.canEdit = user?.role === 'admin';
    });

    this.sectionUpdateSub = this.sectionData.updated.subscribe({
      next: update => {
        if(update === true)
          this.updateSections();

        this.creatingNew = false;
      }
    })
  }

  updateSections() {
    this.sectionData.fetchSectionDetail()
      .subscribe(sections => {
        this.sections = sections
      });
  }

  onSwitchCreateMode(addedNew: boolean) {
    this.creatingNew = !this.creatingNew;
    if(addedNew) {
      this.updateSections();
    }
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
    this.sectionUpdateSub.unsubscribe();
  }
}
