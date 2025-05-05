import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'
import { Issue } from './issue.model';

@Injectable({
  providedIn: 'root'
})
export class IssueService {
  //private apiUrl = 'https://your-api.com/issues'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  // Get all issues
  getIssues(): Observable<Issue[]> {
    return this.http.get<Issue[]>('/Issues');
  }

  //// Get a single issue by ID
  //getIssue(id: number): Observable<Issue> {
  //  return this.http.get<Issue>(`${this.apiUrl}/${id}`);
  //}

  // Create a new issue
  createIssue(issue: Issue): Observable<Issue> {
    return this.http.post<Issue>('/Issues', issue);
  }

  // Update an existing issue
  updateIssue(issue: Issue): Observable<Issue> {
    return this.http.put<Issue>(`${'/Issues'}/${issue.id}`, issue);
  }

  // Delete an issue
  deleteIssue(id: string): Observable<void> {
    return this.http.delete<void>(`${'/Issues'}/${id}`);
  }
}
