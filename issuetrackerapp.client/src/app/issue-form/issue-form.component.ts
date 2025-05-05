import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Issue } from '../issue.model';
import { IssueService } from '../issue.service';

@Component({
  selector: 'app-issue-form',
  standalone: false,
  templateUrl: './issue-form.component.html',
  styleUrl: './issue-form.component.css'
})
export class IssueFormComponent {
  message: string = '';

  constructor(private issueService: IssueService,
    public dialogRef: MatDialogRef<IssueFormComponent>,
    @Inject(MAT_DIALOG_DATA) public issue: Issue) { }

  onSubmit(): void {
    if (this.issue.id) {
      this.issueService.updateIssue(this.issue)
        .subscribe({
          next: (data) => {
            console.log('Issue updated');
          },
          error: (err) => {
            err.error.forEach((item: any) => {
              this.message = this.message + '\n' + item.errorMessage;
            });
          },
          complete: () => {
            console.log('Issue updated');
            this.dialogRef.close(this.issue);
            alert('Issue information updated successfully');
          }
        });
    }
    else {
      this.issueService.createIssue(this.issue)
        .subscribe({
          next: (data) => {
            console.log('Issue added');
          },
          error: (err) => {
            err.error.forEach((item: any) => {
              this.message = this.message + '\n' + item.errorMessage;
            });
          },
          complete: () => {
            console.log('Issue updated');
            this.dialogRef.close(this.issue);
            alert('Issue information added successfully');
          }
        });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
