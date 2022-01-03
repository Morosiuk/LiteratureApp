import { CongregationRole } from "./congregationRole";

export interface User {
  username: string;
  firstName: string;
  surname: string;
  created: Date;
  lastActive: Date;
  congregationRoles: CongregationRole[];
  admin: boolean;
  token: string;
}