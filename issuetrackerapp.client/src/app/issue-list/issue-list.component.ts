import { Component } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { IssueFormComponent } from '../issue-form/issue-form.component';
import { Issue } from '../issue.model';
import { IssueService } from '../issue.service';

@Component({
  selector: 'app-issue-list',
  standalone: false,
  templateUrl: './issue-list.component.html',
  styleUrl: './issue-list.component.css'
})
export class IssueListComponent {
  issues: Issue[] = [];
  filteredIssues: Issue[] = [];
  filterText: string = '';

  constructor(private issueService: IssueService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getIssues();
  }
  getIssues(): void {
    this.issueService.getIssues().subscribe((data) => {
      this.issues = data;
      this.filteredIssues = data;
    });
  }

  filterIssues(): void {
    this.filteredIssues = this.issues.filter(issue =>
      issue.title.toLowerCase().includes(this.filterText.toLowerCase()) ||
      issue.description.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }

  deleteIssue(id: string): void {
    if (confirm("Are you sure to delete this record")) {
      this.issueService.deleteIssue(id).subscribe(() => {
        this.issues = this.issues.filter(issue => issue.id !== id);
        this.filterIssues(); // Re-filter after deletion
      });
    }
    
  }

  addIssue(): void {
    this.openIssueForm({}, false);
  }


  editIssue(editItem: Issue): void {
    let tempData = {...editItem };
    this.openIssueForm(tempData, true);
  }

  openIssueForm(issue?: any, editMode: boolean = true) {
    const dialogRef = this.dialog.open(IssueFormComponent, {
      width: '600px',
      data: issue ? issue : {title:'',description:'',status:'Open',priority:'Medium'}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getIssues();
      }
    });
  }

}
