import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleNavbarComponent } from './role-navbar.component';

describe('RoleNavbarComponent', () => {
  let component: RoleNavbarComponent;
  let fixture: ComponentFixture<RoleNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleNavbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoleNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
