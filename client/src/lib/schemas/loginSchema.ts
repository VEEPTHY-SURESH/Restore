import { z } from "zod"; // using to valid creds

export const loginSchema = z.object({
    email: z.string().email(),
    password: z.string().min(6,{
        message: 'Password must be at leat 6 characters'
    })
});

export type LoginSchema = z.infer<typeof loginSchema>;