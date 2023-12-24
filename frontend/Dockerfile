# Use a node base image
FROM node:latest

# Set the working directory in the container
WORKDIR /app

# Copy the package.json and package-lock.json (if available)
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of your app's source code
COPY . .

# Your app runs on port 3000 by default, expose it
EXPOSE 3000

# Start the app
CMD ["npm", "start"]
