import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

interface Users {
  customerId: number;
  firstName: string;
  lastName: string;
  email: string;
  active: boolean;
}

@Component({
  selector: 'app-user-listing',
  standalone: false,
  templateUrl: './user-listing.component.html',
  styleUrl: './user-listing.component.css'
})
export class UserListingComponent {

  activeUser: Users[] = [];  
  inactiveUser: Users[] = []; 

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  goToAddUser() {
    this.router.navigate(['/user']);
  }

  getUsers() {
    this.userService.getUsers().subscribe((response: Users[]) => {
      console.log('Users:', response);
  
      // Filter users based on active status
      this.activeUser = response.filter(user => user.active === true);  // Active users (1)
      this.inactiveUser = response.filter(user => user.active === false); // Inactive users (0)
      
    }, error => {
      console.error('Error fetching users:', error);
    });
  }

  // Handle Drag-and-Drop between tables
  drop(event: CdkDragDrop<Users[]>, targetList: string) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const user = event.previousContainer.data[event.previousIndex];

      // Remove from previous list and add to the new list
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      // Update user status in the backend
      this.updateUserStatus(user.customerId, targetList);
    }
  }

  // API call to update user status
  updateUserStatus(customerId: number, status: string) {
    const isActive = status === 'active';

    this.userService.updateUserStatus(customerId, isActive).subscribe(
      () => {
        console.log(`User ${customerId} status updated to ${isActive}`);
      },
      (error: any) => {
        console.error('Error updating user status', error);
      }
    );
  }
  
 
}
