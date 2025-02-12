import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface Country {
  countryId: number;
  country1: string;
}

@Component({
  selector: 'app-add-user',
  standalone: false,
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent implements OnInit {

  customerForm!: FormGroup;
  countryData: Country[] = [];
  constructor(private fb: FormBuilder, private userService: UserService) {}

  ngOnInit() {
    this.customerForm = this.fb.group({
      customerId: [0], // Default value for new customer
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      cityName: ['', Validators.required],
      countryId: [null, Validators.required], // Ensure it's a number
      address: ['', Validators.required],
      address2: [''],
      district: ['', Validators.required],
      postalCode: ['', Validators.required],
      phone: ['', Validators.required],
      storeId: [null], // Ensure it's a number or null
      active: [true]
    });
  
    this.getCountries();
  }
  

  onSubmit() {
    if (this.customerForm.valid) {
      this.userService.saveUser(this.customerForm.value).subscribe(response => {
        console.log('User saved successfully!', response);
      }, error => {
        console.error('Error saving user', error);
      });
    } else {
      console.log('Form is invalid');
    }
  }

  getCountries() {
    this.userService.getCountries().subscribe((response: Country[]) => {
      this.countryData = response; // Directly assign the array
    }, error => {
      console.error("Error fetching countries:", error);
    });
  }
  
}
